// import 'reflect-metadata';
import {Constructor} from "../types";

export class PagedResultDto<TEntityDto> {

    // @Reflect.metadata("design:type", Array)
    items: TEntityDto[] | undefined = [];
    
    totalCount: number = 0;

    constructor(public itemType: Constructor<TEntityDto>) {}
}