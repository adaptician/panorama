import {ViewSceneDto} from "../dtos/ViewSceneDto";

export class SceneReceivedEventData {
    data: ViewSceneDto;

    constructor(data: Partial<SceneReceivedEventData> = {}) {
        this.data = data.data || undefined;
    }
}