import {Inject, Injectable, InjectionToken, Optional} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {AutoMapper} from "@shared/service-proxies/common/AutoMapper";
import {PagedResultDto} from "@shared/service-proxies/common/dtos/PagedResultDto";
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {CreateSceneDto} from "@shared/service-proxies/scenography/dtos/CreateSceneDto";
import {UpdateSceneDto} from "@shared/service-proxies/scenography/dtos/UpdateSceneDto";
import {ServiceProxy} from "@shared/service-proxies/common/crud-service-proxy";

export const SCENOGRAPHY_API_BASE_URL = new InjectionToken<string>('SCENOGRAPHY_API_BASE_URL');

@Injectable()
export class SceneServiceProxy extends ServiceProxy {
    
    private _autoMapper: AutoMapper;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(SCENOGRAPHY_API_BASE_URL) baseUrl?: string) {
        super(http, baseUrl);
        
        this._autoMapper = new AutoMapper();
    }

    /**
     * @param keyword (optional)
     * @param skipCount (optional)
     * @param maxResultCount (optional)
     * @return Success
     */
    getAll(keyword: string | undefined, skipCount: number | undefined, maxResultCount: number | undefined): Observable<PagedResultDto<ViewSceneDto>> {
        const path_ = "/scene?";
        
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
        const path_ = "/Scene/";
        
        return super._get(path_, 
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto), 
            id);
    }
    
    /**
     * @param body (optional)
     * @return Success
     */
    create(body: CreateSceneDto | undefined): Observable<ViewSceneDto> {
        const path_ = "/Scene";
        
        return super._create(path_,
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto),
            body);
    }

    /**
     * @param body (optional)
     * @return Success
     */
    update(body: UpdateSceneDto | undefined): Observable<ViewSceneDto> {
        const path_ = "/Scene";
        
        return super._update(path_,
            (resultData) => this._autoMapper.map(resultData, ViewSceneDto),
            body);
    }

    /**
     * @param id (optional)
     * @return Success
     */
    delete(id: number | undefined): Observable<void> {
        const path_ = "/Scene/";
        
        return super._delete(path_, id);
    }
}