import {ViewSceneDto} from "../dtos/ViewSceneDto";
import {PagedResultDto} from "../../common/dtos/PagedResultDto";

export class ScenesReceivedEventData {
    Data: PagedResultDto<ViewSceneDto>;
    
    constructor(data: Partial<ScenesReceivedEventData> = {}) {
        this.Data = data.Data || undefined;
    }
}