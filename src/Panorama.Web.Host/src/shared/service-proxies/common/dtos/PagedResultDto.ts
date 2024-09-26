import {Constructor} from "@shared/service-proxies/common/types";
import 'reflect-metadata';

export class PagedResultDto<TEntityDto> {

    @Reflect.metadata("design:type", Array)
    items: TEntityDto[] | undefined = [];
    
    totalCount: number = 0;

    constructor(public itemType: Constructor<TEntityDto>) {}
}