import {IEntityDto} from "../../common/dtos/EntityDto";

export interface IViewSceneDto extends IEntityDto {
    name: string;
    description: string | undefined;
    scenographyId: number;
    sceneData: string | undefined;
}

export class ViewSceneDto implements IViewSceneDto {
    id: number = undefined;
    name: string = undefined;
    description: string | undefined = undefined;
    scenographyId: number = undefined;
    sceneData: string | undefined = undefined;
}