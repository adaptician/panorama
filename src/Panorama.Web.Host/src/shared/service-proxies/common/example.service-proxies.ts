import {Inject, Injectable, InjectionToken, Optional} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServiceProxy} from "./crud-service-proxy";
import {AutoMapper} from "./AutoMapper";
import {PagedResultDto} from "./dtos/PagedResultDto";
import {ViewSceneDto} from "../service-proxies";
import {UpdateSceneDto} from "../scenography/dtos/UpdateSceneDto";
import {CreateSceneDto} from "../scenography/dtos/CreateSceneDto";

export const API_BASE_URL = "<PLACEHOLDER>";
export const ENTITY_ROOT = "Entity";

/*
* Keeping this here as an example of how to use the ServiceProxy base.
* Replace the PLACEHOLDERS and concrete Types required on generic parameters.
* */

@Injectable()
export class ExampleServiceProxy extends ServiceProxy {
    
    private _autoMapper: AutoMapper;

    // Without module, the constructor parameters error on decorators.
    // UNCOMMENT to implement.
    // constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    //     super(http, baseUrl);
    //    
    //     this._autoMapper = new AutoMapper();
    // }

    /**
     * @param keyword (optional)
     * @param skipCount (optional)
     * @param maxResultCount (optional)
     * @return Success
     */
    getAll(keyword: string | undefined, skipCount: number | undefined, maxResultCount: number | undefined): Observable<PagedResultDto<ViewSceneDto>> {
        const path_ = `/${ENTITY_ROOT}?`;
        
        return super._getAll(path_, 
            (resultData) => this._autoMapper.map(resultData, PagedResultDto<ViewSceneDto>, ViewSceneDto),
            keyword, 
            skipCount, 
            maxResultCount);
    }

    /**
     * @param id (optional)
     * @return Success
     */
    get(id: number | undefined): Observable<ViewSceneDto> {
        const path_ = `/${ENTITY_ROOT}/`;
        
        return super._get(path_, 
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto), 
            id);
    }
    
    /**
     * @param body (optional)
     * @return Success
     */
    create(body: CreateSceneDto | undefined): Observable<ViewSceneDto> {
        const path_ = `/${ENTITY_ROOT}`;
        
        return super._create(path_,
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto),
            body);
    }

    /**
     * @param body (optional)
     * @return Success
     */
    update(body: UpdateSceneDto | undefined): Observable<ViewSceneDto> {
        const path_ = `/${ENTITY_ROOT}`;
        
        return super._update(path_,
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto),
            body);
    }

    /**
     * @param id (optional)
     * @return Success
     */
    delete(id: number | undefined): Observable<void> {
        const path_ = `/${ENTITY_ROOT}/`;
        
        return super._delete(path_, id);
    }
}