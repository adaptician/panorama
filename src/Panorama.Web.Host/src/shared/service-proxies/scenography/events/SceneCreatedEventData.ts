import {ViewSceneDto} from "../dtos/ViewSceneDto";

export class SceneCreatedEventData {
    data: ViewSceneDto;

    constructor(data: Partial<SceneCreatedEventData> = {}) {
        this.data = data.data || undefined;
    }
}