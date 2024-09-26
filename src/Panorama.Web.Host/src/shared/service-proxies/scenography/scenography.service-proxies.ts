import {Inject, Injectable, InjectionToken, Optional} from "@angular/core";
import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {Observable, of as _observableOf, throwError as _observableThrow} from "rxjs";
import {mergeMap as _observableMergeMap} from "rxjs/operators";
import {catchError as _observableCatch} from "rxjs/internal/operators/catchError";
import {
    ApiException,
} from "../service-proxies";
import {AutoMapper} from "@shared/service-proxies/common/AutoMapper";
import {PagedResultDto} from "@shared/service-proxies/common/dtos/PagedResultDto";
import {ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";

export const SCENOGRAPHY_API_BASE_URL = new InjectionToken<string>('SCENOGRAPHY_API_BASE_URL');

@Injectable()
export class SceneServiceProxy {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;
    
    private _autoMapper: AutoMapper;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(SCENOGRAPHY_API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
        
        this._autoMapper = new AutoMapper();
    }

    /**
     * @param keyword (optional)
     * @param skipCount (optional)
     * @param maxResultCount (optional)
     * @return Success
     */
    getAll(keyword: string | undefined, skipCount: number | undefined, maxResultCount: number | undefined): Observable<PagedResultDto<ViewSceneDto>> {
        let url_ = this.baseUrl + "/scene?";
        if (keyword === null)
            throw new Error("The parameter 'keyword' cannot be null.");
        else if (keyword !== undefined)
            url_ += "Keyword=" + encodeURIComponent("" + keyword) + "&";
        if (skipCount === null)
            throw new Error("The parameter 'skipCount' cannot be null.");
        else if (skipCount !== undefined)
            url_ += "SkipCount=" + encodeURIComponent("" + skipCount) + "&";
        if (maxResultCount === null)
            throw new Error("The parameter 'maxResultCount' cannot be null.");
        else if (maxResultCount !== undefined)
            url_ += "MaxResultCount=" + encodeURIComponent("" + maxResultCount) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<PagedResultDto<ViewSceneDto>>;
                }
            } else
                return _observableThrow(response_) as any as Observable<PagedResultDto<ViewSceneDto>>;
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<PagedResultDto<ViewSceneDto>> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
                (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
                let result200: any = null;
                let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = this._autoMapper.map(resultData200, PagedResultDto<ViewSceneDto>, ViewSceneDto);
                return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    /**
     * @param body (optional)
     * @return Success
     */
    // create(body: CreateRoleDto | undefined): Observable<RoleDto> {
    //     let url_ = this.baseUrl + "/api/services/app/Role/Create";
    //     url_ = url_.replace(/[?&]$/, "");
    //
    //     const content_ = JSON.stringify(body);
    //
    //     let options_ : any = {
    //         body: content_,
    //         observe: "response",
    //         responseType: "blob",
    //         headers: new HttpHeaders({
    //             "Content-Type": "application/json-patch+json",
    //             "Accept": "text/plain"
    //         })
    //     };
    //
    //     return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
    //         return this.processCreate(response_);
    //     })).pipe(_observableCatch((response_: any) => {
    //         if (response_ instanceof HttpResponseBase) {
    //             try {
    //                 return this.processCreate(response_ as any);
    //             } catch (e) {
    //                 return _observableThrow(e) as any as Observable<RoleDto>;
    //             }
    //         } else
    //             return _observableThrow(response_) as any as Observable<RoleDto>;
    //     }));
    // }
    //
    // protected processCreate(response: HttpResponseBase): Observable<RoleDto> {
    //     const status = response.status;
    //     const responseBlob =
    //         response instanceof HttpResponse ? response.body :
    //             (response as any).error instanceof Blob ? (response as any).error : undefined;
    //
    //     let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    //     if (status === 200) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             let result200: any = null;
    //             let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
    //             result200 = RoleDto.fromJS(resultData200);
    //             return _observableOf(result200);
    //         }));
    //     } else if (status !== 200 && status !== 204) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    //         }));
    //     }
    //     return _observableOf(null as any);
    // }
    

    /**
     * @param body (optional)
     * @return Success
     */
    // update(body: RoleDto | undefined): Observable<RoleDto> {
    //     let url_ = this.baseUrl + "/api/services/app/Role/Update";
    //     url_ = url_.replace(/[?&]$/, "");
    //
    //     const content_ = JSON.stringify(body);
    //
    //     let options_ : any = {
    //         body: content_,
    //         observe: "response",
    //         responseType: "blob",
    //         headers: new HttpHeaders({
    //             "Content-Type": "application/json-patch+json",
    //             "Accept": "text/plain"
    //         })
    //     };
    //
    //     return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
    //         return this.processUpdate(response_);
    //     })).pipe(_observableCatch((response_: any) => {
    //         if (response_ instanceof HttpResponseBase) {
    //             try {
    //                 return this.processUpdate(response_ as any);
    //             } catch (e) {
    //                 return _observableThrow(e) as any as Observable<RoleDto>;
    //             }
    //         } else
    //             return _observableThrow(response_) as any as Observable<RoleDto>;
    //     }));
    // }
    //
    // protected processUpdate(response: HttpResponseBase): Observable<RoleDto> {
    //     const status = response.status;
    //     const responseBlob =
    //         response instanceof HttpResponse ? response.body :
    //             (response as any).error instanceof Blob ? (response as any).error : undefined;
    //
    //     let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    //     if (status === 200) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             let result200: any = null;
    //             let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
    //             result200 = RoleDto.fromJS(resultData200);
    //             return _observableOf(result200);
    //         }));
    //     } else if (status !== 200 && status !== 204) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    //         }));
    //     }
    //     return _observableOf(null as any);
    // }

    /**
     * @param id (optional)
     * @return Success
     */
    // delete(id: number | undefined): Observable<void> {
    //     let url_ = this.baseUrl + "/api/services/app/Role/Delete?";
    //     if (id === null)
    //         throw new Error("The parameter 'id' cannot be null.");
    //     else if (id !== undefined)
    //         url_ += "Id=" + encodeURIComponent("" + id) + "&";
    //     url_ = url_.replace(/[?&]$/, "");
    //
    //     let options_ : any = {
    //         observe: "response",
    //         responseType: "blob",
    //         headers: new HttpHeaders({
    //         })
    //     };
    //
    //     return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
    //         return this.processDelete(response_);
    //     })).pipe(_observableCatch((response_: any) => {
    //         if (response_ instanceof HttpResponseBase) {
    //             try {
    //                 return this.processDelete(response_ as any);
    //             } catch (e) {
    //                 return _observableThrow(e) as any as Observable<void>;
    //             }
    //         } else
    //             return _observableThrow(response_) as any as Observable<void>;
    //     }));
    // }
    //
    // protected processDelete(response: HttpResponseBase): Observable<void> {
    //     const status = response.status;
    //     const responseBlob =
    //         response instanceof HttpResponse ? response.body :
    //             (response as any).error instanceof Blob ? (response as any).error : undefined;
    //
    //     let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    //     if (status === 200) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             return _observableOf(null as any);
    //         }));
    //     } else if (status !== 200 && status !== 204) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    //         }));
    //     }
    //     return _observableOf(null as any);
    // }

    /**
     * @param id (optional)
     * @return Success
     */
    // get(id: number | undefined): Observable<RoleDto> {
    //     let url_ = this.baseUrl + "/api/services/app/Role/Get?";
    //     if (id === null)
    //         throw new Error("The parameter 'id' cannot be null.");
    //     else if (id !== undefined)
    //         url_ += "Id=" + encodeURIComponent("" + id) + "&";
    //     url_ = url_.replace(/[?&]$/, "");
    //
    //     let options_ : any = {
    //         observe: "response",
    //         responseType: "blob",
    //         headers: new HttpHeaders({
    //             "Accept": "text/plain"
    //         })
    //     };
    //
    //     return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
    //         return this.processGet(response_);
    //     })).pipe(_observableCatch((response_: any) => {
    //         if (response_ instanceof HttpResponseBase) {
    //             try {
    //                 return this.processGet(response_ as any);
    //             } catch (e) {
    //                 return _observableThrow(e) as any as Observable<RoleDto>;
    //             }
    //         } else
    //             return _observableThrow(response_) as any as Observable<RoleDto>;
    //     }));
    // }
    //
    // protected processGet(response: HttpResponseBase): Observable<RoleDto> {
    //     const status = response.status;
    //     const responseBlob =
    //         response instanceof HttpResponse ? response.body :
    //             (response as any).error instanceof Blob ? (response as any).error : undefined;
    //
    //     let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
    //     if (status === 200) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             let result200: any = null;
    //             let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
    //             result200 = RoleDto.fromJS(resultData200);
    //             return _observableOf(result200);
    //         }));
    //     } else if (status !== 200 && status !== 204) {
    //         return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
    //             return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    //         }));
    //     }
    //     return _observableOf(null as any);
    // }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}