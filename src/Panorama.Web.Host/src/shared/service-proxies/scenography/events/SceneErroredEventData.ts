import {ErrorDto} from "../../common/dtos/ErrorDto";

export class SceneErroredEventData {
    error: ErrorDto
    
    constructor(data: Partial<SceneErroredEventData> = {}) {
        this.error = data.error || undefined;
    }
}