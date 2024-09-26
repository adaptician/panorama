import {PagedRequestDto} from "@shared/paged-listing-component-base";

export class FilterablePagedRequestDto extends PagedRequestDto {
    keyword: string;
}