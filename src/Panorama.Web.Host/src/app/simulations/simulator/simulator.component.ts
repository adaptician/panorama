import {Component, ElementRef, Injector, NgZone, OnInit, ViewChild} from '@angular/core';
import {appModuleAnimation} from "@shared/animations/routerTransition";
import * as THREE from 'three';
import {AppComponentBase} from "@shared/app-component-base";
import {AppEvents} from "@shared/AppEvents";
import {SceneRetrievedEventData} from "@shared/service-proxies/scenography/events/SceneRetrievedEventData";
import {SceneServiceProxy} from "@shared/service-proxies/service-proxies";
import {finalize} from "rxjs/operators";
import {CameraFactory} from "@shared/factories/camera.factory";
import {MeshFactory} from "@shared/factories/mesh.factory";
import {PrototypeRegistry} from "@shared/registries/prototype.registry";

@Component({
    selector: 'sim-simulator',
    templateUrl: './simulator.component.html',
    styleUrl: './simulator.component.less',
    animations: [appModuleAnimation()]
})
export class SimulatorComponent extends AppComponentBase implements OnInit {

    //#region Element References

    @ViewChild('projectionCanvas')
    private _projectionCanvasRef: ElementRef;

    private get projectionCanvas(): HTMLCanvasElement {
        return this._projectionCanvasRef.nativeElement;
    }

    //#endregion

    //#region Renderers

    private _projectionRenderer!: THREE.WebGLRenderer;
    
    //#endregion Renderers

    //#region Cameras

    private _projectionCamera!: THREE.Camera;

    //#endregion Cameras
    
    //#region Scenes

    // TODO:T explain why these are needed.
    private _projectionScene!: THREE.Scene;

    //#endregion
        
    //#region Trackers

    private _clock = new THREE.Clock();
    private _oldElapsedTime = 0;
    
    private _projectionMeshTrackers: THREE.Mesh[] = [];
    
    //#endregion

    private _isRenderingActive: boolean = false;

    private _cameraFactory: CameraFactory;
    private _meshFactory: MeshFactory;
    private _prototypeRegistry: PrototypeRegistry;
    
    constructor(
        injector: Injector,
        private _sceneService: SceneServiceProxy,
        private _zone: NgZone
    ) {
        super(injector);

        // TODO:T make this injectable rather;
        this._cameraFactory = new CameraFactory();
        this._meshFactory = new MeshFactory();
        this._prototypeRegistry = new PrototypeRegistry();
    }

    ngOnInit() {
        // TODO:T retrieve this from query parameter.
        this.onGetScene(); // TODO:T this doesn't work - think it's a timing thing on signalr not being ready.
        this.subscribeToEvents();
    }

    onGetScene() {
        this.getScene('8d30eac7-a55b-49b6-a582-70d61923db8e');
    }
    
    private subscribeToEvents(): void {

        this.subscribeToEvent(AppEvents.SignalR_AppEvents_Scene_Retrieved_Trigger,
            (json) => {
                const data = new SceneRetrievedEventData();
                Object.assign(data, JSON.parse(json));

                this._zone.run(() => {
                    this.handleSceneRetrieved(data);
                });
            });

    }

    private getScene(correlationId: string): void {
        this.setBusy('loading', true);

        this._sceneService
            .commandGetSceneById(correlationId)
            .pipe(finalize(() => this.setBusy('loading', false)))
            .subscribe(() => {
            });
    }

    private handleSceneRetrieved(data: SceneRetrievedEventData): void {
        this.setBusy('loading', false);
        
        if (data?.data) {

            const json = data.data.sceneData;
            this.updateRendering(json);
        }
    }

    private updateRendering(metaJson: string): void {
        this.setBusy('rendering', true);

        if (this._isRenderingActive) {
            // TODO:T what happens when the scene is already rendered?
        } else {
            this.createRendering(metaJson);
        }

        this.setBusy('rendering', false); // TODO:T where should this go?
    }

    private createRendering(metaJson: string): void {
        console.log(`RENDER ${metaJson}`);
        this.initScene(metaJson);

        this.setupFloor();
        this.addAmbientLight();
        this.setupLight();

        this.startRenderingLoop();

        this.tick();
    }
    
    //#region Scene Instantiation
    
    private initScene(metaJson: string) {
        
        this._projectionScene = this.parseScene(metaJson, this._projectionMeshTrackers, this._projectionCamera);

        // TODO:T move this color to a variable somewhere.
        this._projectionScene.background = new THREE.Color( 0x25274d );

        if (!this._projectionCamera) {
            // No camera was parsed into the scene; create one.
            this._projectionCamera = this._cameraFactory.createPerspectiveCamera(this.projectionCanvas);
        }
    }

    private parseScene(metaJson: string, meshRefs: THREE.Mesh[], camera: THREE.Camera): THREE.Scene {
        if (!metaJson || metaJson.length <= 0) return;

        const loader = new THREE.ObjectLoader();

        try {
            const json = JSON.parse(metaJson);
            const data = loader.parse(json);

            const scene = new THREE.Scene();
            scene.add(data);

            this.parse3DObjects(scene, meshRefs, camera, scene);

            return scene;

        } catch (err) {
            throw Error(`Unable to populate scene - scene data is invalid. \m ${JSON.stringify(err)}`);
        }
    }

    private parse3DObjects(parent: THREE.Object3D, meshRefs: THREE.Mesh[], camera: THREE.Camera, scene: THREE.Scene) {

        if (parent && parent.children && parent.children.length > 0) {
            parent.children.forEach(obj => {

                const isCamera = this._cameraFactory.isCamera(obj);
                if (isCamera) {
                    camera = this._cameraFactory.castOut(obj);
                    scene.add(camera);
                    return; // continue
                }

                const isMesh = this._meshFactory.isMesh(obj);
                if (isMesh) {
                    const mesh = this._meshFactory.castOut(obj);
                    meshRefs.push(mesh);
                    scene.add(mesh);

                    return; // continue
                }

                this.parse3DObjects(obj, meshRefs, camera, scene);
            });
        }

    }

    private setupFloor() {
        const geometry = this._prototypeRegistry.planeGeometry;
        const material = this._prototypeRegistry.darkConcreteMaterial;
        material.visible = true;
        
        let floor: THREE.Mesh = new THREE.Mesh(geometry, material);
        floor.receiveShadow = true;
        floor.rotation.x = - Math.PI * 0.5;
        this._projectionScene.add(floor);
    }

    private addAmbientLight() {
        const ambientLight: THREE.AmbientLight = new THREE.AmbientLight(0xffffff, 0.7);
        this._projectionScene.add(ambientLight);
    }

    private setupLight() {
        let directionalLight = new THREE.DirectionalLight(0xffffff, 0.2);
        
        directionalLight.castShadow = true
        directionalLight.shadow.mapSize.set(1024, 1024)
        directionalLight.shadow.camera.far = 15
        directionalLight.shadow.camera.left = - 7
        directionalLight.shadow.camera.top = 7
        directionalLight.shadow.camera.right = 7
        directionalLight.shadow.camera.bottom = - 7
        directionalLight.position.set(5, 5, 5)
        this._projectionCamera.add(directionalLight)
    }

    //#endregion
    
    //#region Rendering Loop

    private startRenderingLoop() {
        //* Renderer
        this._projectionRenderer = new THREE.WebGLRenderer({ canvas: this.projectionCanvas });
        this._projectionRenderer.shadowMap.enabled = true
        this._projectionRenderer.shadowMap.type = THREE.PCFSoftShadowMap
        this._projectionRenderer.setPixelRatio(devicePixelRatio);
        this._projectionRenderer.setSize(this.projectionCanvas.clientWidth, this.projectionCanvas.clientHeight);


        let component: SimulatorComponent = this;
        (function render() {
            requestAnimationFrame(render);
            component._projectionRenderer.render(component._projectionScene, component._projectionCamera);
        }());
    }

    private tick = () =>
    {
        const elapsedTime = this._clock.getElapsedTime();
        const deltaTime = elapsedTime - this._oldElapsedTime;
        this._oldElapsedTime = elapsedTime;
    }
    
    //#endregion
}
