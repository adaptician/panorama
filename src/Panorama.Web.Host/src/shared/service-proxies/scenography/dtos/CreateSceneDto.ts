export interface ICreateSceneDto {
    name: string;
    description: string | undefined;
    initialSceneData: string | undefined;
}

export class CreateSceneDto implements ICreateSceneDto {
    name: string = undefined;
    description: string | undefined = undefined;
    initialSceneData: string | undefined = undefined;
}