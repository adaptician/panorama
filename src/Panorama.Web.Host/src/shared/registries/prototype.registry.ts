import * as THREE from 'three';
import {THREEConstants} from "@shared/THREE.constants";
import {Injectable} from "@angular/core";

@Injectable()
export class PrototypeRegistry {

    //#region Textures

    private _cubeTextureLoader = new THREE.CubeTextureLoader();

    // #endregion Textures

    // #region Materials

    private _concrete_StandardMaterial_0 = new THREE.MeshStandardMaterial({
        color: THREEConstants.Colors.concrete_0,
        metalness: 0.3,
        roughness: 0.4,
        // envMap: this._concreteCubeMapTexture,
        // envMapIntensity: 0.5
    });
    private _concrete_StandardMaterial_50 = new THREE.MeshStandardMaterial({
        color: THREEConstants.Colors.concrete_50,
        metalness: 0.3,
        roughness: 0.4,
        // envMap: this._concreteCubeMapTexture,
        // envMapIntensity: 0.5
    });
    private _concrete_StandardMaterial_100 = new THREE.MeshStandardMaterial({
        color: THREEConstants.Colors.concrete_100,
        metalness: 0.3,
        roughness: 0.4,
        // envMap: this._concreteCubeMapTexture,
        // envMapIntensity: 0.5
    });
    
    public cloneMaterial(colorName: string, huePercent?: number) {

        // e.g. 0, 0.1, 0.3, 0.5, 0.8, 1
        const choice = huePercent ? huePercent :  Math.random();
        
        const dividend = 100;
        const divisor = 3;
        const quotient = Math.floor(dividend / divisor);
        
        // e.g. 1, 2, 3
        let increment = 0; 
        
        while (increment < divisor) {
            if (choice < (quotient / 100))
                return this._standardMaterials[`${colorName}_${huePercent*100}`];
            
            increment += 1;
        }
        
        // Return default;
        return this._standardMaterials[`default_50`];
    }
    
    private _standardMaterials: { [key: string]: THREE.MeshStandardMaterial } = {
        "default_50": this._concrete_StandardMaterial_50.clone(),
        // CONCRETE
        "concrete_0": this._concrete_StandardMaterial_0.clone(),
        "concrete_50": this._concrete_StandardMaterial_50.clone(),
        "concrete_100": this._concrete_StandardMaterial_100.clone(),
    };

    // #endregion Materials

    // #region Geometries

    private _planeGeometry = new THREE.PlaneGeometry(10, 10);
    public get planeGeometry(): THREE.PlaneGeometry {
        return (this._planeGeometry.clone() as THREE.PlaneGeometry);
    }

    private _circleGeometry = new THREE.CircleGeometry(5, 32)
    public get circleGeometry(): THREE.CircleGeometry {
        return (this._circleGeometry.clone() as THREE.CircleGeometry);
    }

    private _sphereGeometry = new THREE.SphereGeometry(1, 20, 20);
    public get sphereGeometry(): THREE.SphereGeometry {
        return (this._sphereGeometry.clone() as THREE.SphereGeometry);
    }

    private _boxGeometry = new THREE.BoxGeometry(1, 1, 1);
    public get boxGeometry(): THREE.BoxGeometry {
        return (this._boxGeometry.clone() as THREE.BoxGeometry);
    }

    private _cylinderGeometry = new THREE.CylinderGeometry(1, 1, 2, 20);
    public get cylinderGeometry(): THREE.CylinderGeometry {
        return (this._cylinderGeometry.clone() as THREE.CylinderGeometry);
    }

    private _torusGeometry = new THREE.TorusGeometry(1, 0.4, 16, 60);
    public get torusGeometry(): THREE.TorusGeometry {
        return (this._torusGeometry.clone() as THREE.TorusGeometry);
    }

    private _coneGeometry = new THREE.ConeGeometry(1, 2, 32);
    public get coneGeometry(): THREE.ConeGeometry {
        return (this._coneGeometry.clone() as THREE.ConeGeometry);
    }


    // #endregion Geometries


    constructor () {

    }
}