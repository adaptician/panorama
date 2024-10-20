import {ViewSceneDto} from "../dtos/ViewSceneDto";

export class SceneRetrievedEventData {
    data: ViewSceneDto;

    constructor(data: Partial<SceneRetrievedEventData> = {}) {
        this.data = data.data || undefined;
    }
}