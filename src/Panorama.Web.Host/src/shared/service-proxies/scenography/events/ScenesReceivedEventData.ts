import {ViewSceneDto} from "../dtos/ViewSceneDto";
import {PagedResultDto} from "../../common/dtos/PagedResultDto";

export class ScenesReceivedEventData {
    data: PagedResultDto<ViewSceneDto>;
    
    constructor(data: Partial<ScenesReceivedEventData> = {}) {
        this.data = data.data || undefined;
    }
}