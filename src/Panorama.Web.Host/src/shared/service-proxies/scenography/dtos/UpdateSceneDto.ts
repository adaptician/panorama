import {IEntityDto} from "@shared/service-proxies/common/dtos/EntityDto";

export interface IUpdateSceneDto extends IEntityDto {
    name: string;
    description: string | undefined;
}

export class UpdateSceneDto implements IUpdateSceneDto {
    id: number = 0;
    name: string = undefined;
    description: string | undefined = undefined;
}