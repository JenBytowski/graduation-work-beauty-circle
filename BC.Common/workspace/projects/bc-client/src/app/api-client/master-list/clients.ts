/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.7.0.0 (NJsonSchema v10.1.24.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class FilesClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @return Success
     */
    filesGet(name: string | null): Observable<void> {
        let url_ = this.baseUrl + "/files/{name}";
        if (name === undefined || name === null)
            throw new Error("The parameter 'name' must be defined.");
        url_ = url_.replace("{name}", encodeURIComponent("" + name));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processFilesGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processFilesGet(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    /**
     * @param isTemporary (optional)
     * @return Success
     */
    filesPost(isTemporary: boolean | undefined): Observable<void> {
        let url_ = this.baseUrl + "/files?";
        if (isTemporary === null)
            throw new Error("The parameter 'isTemporary' cannot be null.");
        else if (isTemporary !== undefined)
            url_ += "isTemporary=" + encodeURIComponent("" + isTemporary) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processFilesPost(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processFilesPost(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    /**
     * @return Success
     */
    filesDelete(name: string | null): Observable<void> {
        let url_ = this.baseUrl + "/files/{name}";
        if (name === undefined || name === null)
            throw new Error("The parameter 'name' must be defined.");
        url_ = url_.replace("{name}", encodeURIComponent("" + name));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processFilesDelete(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processFilesDelete(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processFilesGet(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    protected processFilesDelete(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    protected processFilesPost(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }
}

@Injectable()
export class MasterListClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @param cityId (optional)
     * @param serviceTypeIds (optional)
     * @param startHour (optional)
     * @param endHour (optional)
     * @return Success
     */
    mastersListGet(cityId: string | null | undefined, serviceTypeIds: string[] | null | undefined, startHour: number | null | undefined, endHour: number | null | undefined): Observable<MasterRes[]> {
        let url_ = this.baseUrl + "/masters-list?";
        if (cityId !== undefined && cityId !== null)
            url_ += "CityId=" + encodeURIComponent("" + cityId) + "&";
        if (serviceTypeIds !== undefined && serviceTypeIds !== null)
            serviceTypeIds && serviceTypeIds.forEach(item => { url_ += "ServiceTypeIds=" + encodeURIComponent("" + item) + "&"; });
        if (startHour !== undefined && startHour !== null)
            url_ += "StartHour=" + encodeURIComponent("" + startHour) + "&";
        if (endHour !== undefined && endHour !== null)
            url_ += "EndHour=" + encodeURIComponent("" + endHour) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processMastersListGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processMastersListGet(<any>response_);
                } catch (e) {
                    return <Observable<MasterRes[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<MasterRes[]>><any>_observableThrow(response_);
        }));
    }

    /**
     * @param file (optional)
     * @return Success
     */
    avatar(id: string, file: FileParameter | null | undefined): Observable<void> {
        let url_ = this.baseUrl + "/masters-list/{id}/avatar";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("file", file.data, file.fileName ? file.fileName : "file");

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAvatar(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAvatar(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    /**
     * @return Success
     */
    mastersListPut(id: string): Observable<void> {
        let url_ = this.baseUrl + "/masters-list/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processMastersListPut(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processMastersListPut(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processMastersListGet(response: HttpResponseBase): Observable<MasterRes[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(MasterRes.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<MasterRes[]>(<any>null);
    }

    protected processMastersListPut(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    protected processAvatar(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }
}

@Injectable()
export class PreClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @param isTemporary (optional)
     * @return Success
     */
    pre(isTemporary: boolean | undefined): Observable<void> {
        let url_ = this.baseUrl + "/pre?";
        if (isTemporary === null)
            throw new Error("The parameter 'isTemporary' cannot be null.");
        else if (isTemporary !== undefined)
            url_ += "isTemporary=" + encodeURIComponent("" + isTemporary) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPre(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPre(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPre(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }
}

export class PriceListItem implements IPriceListItem {
    name?: string | undefined;
    priceMin?: number;
    priceMax?: number;
    durationInMinutesMax?: number;

    constructor(data?: IPriceListItem) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.priceMin = _data["priceMin"];
            this.priceMax = _data["priceMax"];
            this.durationInMinutesMax = _data["durationInMinutesMax"];
        }
    }

    static fromJS(data: any): PriceListItem {
        data = typeof data === 'object' ? data : {};
        let result = new PriceListItem();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["priceMin"] = this.priceMin;
        data["priceMax"] = this.priceMax;
        data["durationInMinutesMax"] = this.durationInMinutesMax;
        return data;
    }
}

export interface IPriceListItem {
    name?: string | undefined;
    priceMin?: number;
    priceMax?: number;
    durationInMinutesMax?: number;
}

export enum DayOfWeek {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _5 = 5,
    _6 = 6,
}

export class Window implements IWindow {
    startTime?: Date;
    endTime?: Date;

    constructor(data?: IWindow) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): Window {
        data = typeof data === 'object' ? data : {};
        let result = new Window();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        return data;
    }
}

export interface IWindow {
    startTime?: Date;
    endTime?: Date;
}

export class ScheduleDay implements IScheduleDay {
    dayOfWeek?: DayOfWeek;
    startTime?: Date;
    endTime?: Date;
    windows?: Window[] | undefined;

    constructor(data?: IScheduleDay) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.dayOfWeek = _data["dayOfWeek"];
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
            if (Array.isArray(_data["windows"])) {
                this.windows = [] as any;
                for (let item of _data["windows"])
                    this.windows!.push(Window.fromJS(item));
            }
        }
    }

    static fromJS(data: any): ScheduleDay {
        data = typeof data === 'object' ? data : {};
        let result = new ScheduleDay();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["dayOfWeek"] = this.dayOfWeek;
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        if (Array.isArray(this.windows)) {
            data["windows"] = [];
            for (let item of this.windows)
                data["windows"].push(item.toJSON());
        }
        return data;
    }
}

export interface IScheduleDay {
    dayOfWeek?: DayOfWeek;
    startTime?: Date;
    endTime?: Date;
    windows?: Window[] | undefined;
}

export class MasterRes implements IMasterRes {
    name?: string | undefined;
    cityId?: string | undefined;
    avatarUrl?: string | undefined;
    about?: string | undefined;
    address?: string | undefined;
    phone?: string | undefined;
    instagramProfile?: string | undefined;
    vkProfile?: string | undefined;
    viber?: string | undefined;
    skype?: string | undefined;
    speciality?: string | undefined;
    priceList?: PriceListItem[] | undefined;
    schedule?: ScheduleDay[] | undefined;
    averageRating?: number;

    constructor(data?: IMasterRes) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.cityId = _data["cityId"];
            this.avatarUrl = _data["avatarUrl"];
            this.about = _data["about"];
            this.address = _data["address"];
            this.phone = _data["phone"];
            this.instagramProfile = _data["instagramProfile"];
            this.vkProfile = _data["vkProfile"];
            this.viber = _data["viber"];
            this.skype = _data["skype"];
            this.speciality = _data["speciality"];
            if (Array.isArray(_data["priceList"])) {
                this.priceList = [] as any;
                for (let item of _data["priceList"])
                    this.priceList!.push(PriceListItem.fromJS(item));
            }
            if (Array.isArray(_data["schedule"])) {
                this.schedule = [] as any;
                for (let item of _data["schedule"])
                    this.schedule!.push(ScheduleDay.fromJS(item));
            }
            this.averageRating = _data["averageRating"];
        }
    }

    static fromJS(data: any): MasterRes {
        data = typeof data === 'object' ? data : {};
        let result = new MasterRes();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["cityId"] = this.cityId;
        data["avatarUrl"] = this.avatarUrl;
        data["about"] = this.about;
        data["address"] = this.address;
        data["phone"] = this.phone;
        data["instagramProfile"] = this.instagramProfile;
        data["vkProfile"] = this.vkProfile;
        data["viber"] = this.viber;
        data["skype"] = this.skype;
        data["speciality"] = this.speciality;
        if (Array.isArray(this.priceList)) {
            data["priceList"] = [];
            for (let item of this.priceList)
                data["priceList"].push(item.toJSON());
        }
        if (Array.isArray(this.schedule)) {
            data["schedule"] = [];
            for (let item of this.schedule)
                data["schedule"].push(item.toJSON());
        }
        data["averageRating"] = this.averageRating;
        return data;
    }
}

export interface IMasterRes {
    name?: string | undefined;
    cityId?: string | undefined;
    avatarUrl?: string | undefined;
    about?: string | undefined;
    address?: string | undefined;
    phone?: string | undefined;
    instagramProfile?: string | undefined;
    vkProfile?: string | undefined;
    viber?: string | undefined;
    skype?: string | undefined;
    speciality?: string | undefined;
    priceList?: PriceListItem[] | undefined;
    schedule?: ScheduleDay[] | undefined;
    averageRating?: number;
}

export interface FileParameter {
    data: any;
    fileName: string;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    return _observableThrow(new ApiException(message, status, response, headers, result));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}
