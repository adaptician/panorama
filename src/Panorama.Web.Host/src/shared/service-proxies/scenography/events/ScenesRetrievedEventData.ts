import {ViewSceneDto} from "../dtos/ViewSceneDto";
import {PagedResultDto} from "../../common/dtos/PagedResultDto";

export class ScenesRetrievedEventData {
    data: PagedResultDto<ViewSceneDto>;
    
    constructor(data: Partial<ScenesRetrievedEventData> = {}) {
        this.data = data.data || undefined;
    }
}