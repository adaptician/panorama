import {ViewSceneDto} from "../dtos/ViewSceneDto";

export class SceneUpdatedEventData {
    data: ViewSceneDto;

    constructor(data: Partial<SceneUpdatedEventData> = {}) {
        this.data = data.data || undefined;
    }
}