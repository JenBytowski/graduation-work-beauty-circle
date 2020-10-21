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
export class BookingClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * @param masterId (optional)
     * @return Success
     */
    getSchedule(masterId: string | undefined): Observable<GetScheduleRes> {
        let url_ = this.baseUrl + "/booking/get-schedule?";
        if (masterId === null)
            throw new Error("The parameter 'masterId' cannot be null.");
        else if (masterId !== undefined)
            url_ += "masterId=" + encodeURIComponent("" + masterId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSchedule(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSchedule(<any>response_);
                } catch (e) {
                    return <Observable<GetScheduleRes>><any>_observableThrow(e);
                }
            } else
                return <Observable<GetScheduleRes>><any>_observableThrow(response_);
        }));
    }

    protected processGetSchedule(response: HttpResponseBase): Observable<GetScheduleRes> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = GetScheduleRes.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<GetScheduleRes>(<any>null);
    }

    /**
     * @param body (optional)
     * @return Success
     */
    addWorkingWeek(body: AddWorkingWeekReq | undefined): Observable<void> {
        let url_ = this.baseUrl + "/booking/add-working-week";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAddWorkingWeek(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAddWorkingWeek(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processAddWorkingWeek(response: HttpResponseBase): Observable<void> {
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

    /**
     * @param body (optional)
     * @return Success
     */
    addBooking(body: AddBookingReq | undefined): Observable<void> {
        let url_ = this.baseUrl + "/booking/add-booking";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAddBooking(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAddBooking(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processAddBooking(response: HttpResponseBase): Observable<void> {
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

    /**
     * @param body (optional)
     * @return Success
     */
    cancelBooking(body: CancelBookingReq | undefined): Observable<void> {
        let url_ = this.baseUrl + "/booking/cancel-booking";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCancelBooking(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCancelBooking(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processCancelBooking(response: HttpResponseBase): Observable<void> {
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

    /**
     * @param body (optional)
     * @return Success
     */
    addPause(body: AddPauseReq | undefined): Observable<void> {
        let url_ = this.baseUrl + "/booking/add-pause";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAddPause(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAddPause(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processAddPause(response: HttpResponseBase): Observable<void> {
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

    /**
     * @param body (optional)
     * @return Success
     */
    cancelPause(body: CancelPauseReq | undefined): Observable<void> {
        let url_ = this.baseUrl + "/booking/cancel-pause";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCancelPause(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCancelPause(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processCancelPause(response: HttpResponseBase): Observable<void> {
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

export class ScheduleDayItemRes implements IScheduleDayItemRes {
    id?: string;
    scheduleDayId?: string;
    scheduleDayRes?: ScheduleDayRes;
    startTime?: Date;
    endTime?: Date;

    constructor(data?: IScheduleDayItemRes) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.scheduleDayId = _data["scheduleDayId"];
            this.scheduleDayRes = _data["scheduleDayRes"] ? ScheduleDayRes.fromJS(_data["scheduleDayRes"]) : <any>undefined;
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): ScheduleDayItemRes {
        data = typeof data === 'object' ? data : {};
        let result = new ScheduleDayItemRes();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["scheduleDayId"] = this.scheduleDayId;
        data["scheduleDayRes"] = this.scheduleDayRes ? this.scheduleDayRes.toJSON() : <any>undefined;
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        return data;
    }
}

export interface IScheduleDayItemRes {
    id?: string;
    scheduleDayId?: string;
    scheduleDayRes?: ScheduleDayRes;
    startTime?: Date;
    endTime?: Date;
}

export class ScheduleDayRes implements IScheduleDayRes {
    id?: string;
    scheduleId?: string;
    startTime?: Date;
    endTime?: Date;
    items?: ScheduleDayItemRes[] | undefined;

    constructor(data?: IScheduleDayRes) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.scheduleId = _data["scheduleId"];
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(ScheduleDayItemRes.fromJS(item));
            }
        }
    }

    static fromJS(data: any): ScheduleDayRes {
        data = typeof data === 'object' ? data : {};
        let result = new ScheduleDayRes();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["scheduleId"] = this.scheduleId;
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        if (Array.isArray(this.items)) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }
}

export interface IScheduleDayRes {
    id?: string;
    scheduleId?: string;
    startTime?: Date;
    endTime?: Date;
    items?: ScheduleDayItemRes[] | undefined;
}

export class GetScheduleRes implements IGetScheduleRes {
    days?: ScheduleDayRes[] | undefined;

    constructor(data?: IGetScheduleRes) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["days"])) {
                this.days = [] as any;
                for (let item of _data["days"])
                    this.days!.push(ScheduleDayRes.fromJS(item));
            }
        }
    }

    static fromJS(data: any): GetScheduleRes {
        data = typeof data === 'object' ? data : {};
        let result = new GetScheduleRes();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.days)) {
            data["days"] = [];
            for (let item of this.days)
                data["days"].push(item.toJSON());
        }
        return data;
    }
}

export interface IGetScheduleRes {
    days?: ScheduleDayRes[] | undefined;
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

export class AddWorkingWeekReq implements IAddWorkingWeekReq {
    masterId?: string;
    mondayDate?: Date;
    mondayDateOfPausesDonorWeek?: Date | undefined;
    daysToWork?: DayOfWeek[] | undefined;
    startTime?: string | undefined;
    endTime?: string | undefined;

    constructor(data?: IAddWorkingWeekReq) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.masterId = _data["masterId"];
            this.mondayDate = _data["mondayDate"] ? new Date(_data["mondayDate"].toString()) : <any>undefined;
            this.mondayDateOfPausesDonorWeek = _data["mondayDateOfPausesDonorWeek"] ? new Date(_data["mondayDateOfPausesDonorWeek"].toString()) : <any>undefined;
            if (Array.isArray(_data["daysToWork"])) {
                this.daysToWork = [] as any;
                for (let item of _data["daysToWork"])
                    this.daysToWork!.push(item);
            }
            this.startTime = _data["startTime"];
            this.endTime = _data["endTime"];
        }
    }

    static fromJS(data: any): AddWorkingWeekReq {
        data = typeof data === 'object' ? data : {};
        let result = new AddWorkingWeekReq();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["masterId"] = this.masterId;
        data["mondayDate"] = this.mondayDate ? this.mondayDate.toISOString() : <any>undefined;
        data["mondayDateOfPausesDonorWeek"] = this.mondayDateOfPausesDonorWeek ? this.mondayDateOfPausesDonorWeek.toISOString() : <any>undefined;
        if (Array.isArray(this.daysToWork)) {
            data["daysToWork"] = [];
            for (let item of this.daysToWork)
                data["daysToWork"].push(item);
        }
        data["startTime"] = this.startTime;
        data["endTime"] = this.endTime;
        return data;
    }
}

export interface IAddWorkingWeekReq {
    masterId?: string;
    mondayDate?: Date;
    mondayDateOfPausesDonorWeek?: Date | undefined;
    daysToWork?: DayOfWeek[] | undefined;
    startTime?: string | undefined;
    endTime?: string | undefined;
}

export class AddBookingReq implements IAddBookingReq {
    masterId?: string;
    clientId?: string;
    serviceType?: string;
    description?: string | undefined;
    startTime?: Date;
    endTime?: Date;

    constructor(data?: IAddBookingReq) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.masterId = _data["masterId"];
            this.clientId = _data["clientId"];
            this.serviceType = _data["serviceType"];
            this.description = _data["description"];
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): AddBookingReq {
        data = typeof data === 'object' ? data : {};
        let result = new AddBookingReq();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["masterId"] = this.masterId;
        data["clientId"] = this.clientId;
        data["serviceType"] = this.serviceType;
        data["description"] = this.description;
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        return data;
    }
}

export interface IAddBookingReq {
    masterId?: string;
    clientId?: string;
    serviceType?: string;
    description?: string | undefined;
    startTime?: Date;
    endTime?: Date;
}

export class CancelBookingReq implements ICancelBookingReq {
    bookingId?: string;

    constructor(data?: ICancelBookingReq) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.bookingId = _data["bookingId"];
        }
    }

    static fromJS(data: any): CancelBookingReq {
        data = typeof data === 'object' ? data : {};
        let result = new CancelBookingReq();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["bookingId"] = this.bookingId;
        return data;
    }
}

export interface ICancelBookingReq {
    bookingId?: string;
}

export class AddPauseReq implements IAddPauseReq {
    masterId?: string;
    startTime?: Date;
    endTime?: Date;
    description?: string | undefined;

    constructor(data?: IAddPauseReq) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.masterId = _data["masterId"];
            this.startTime = _data["startTime"] ? new Date(_data["startTime"].toString()) : <any>undefined;
            this.endTime = _data["endTime"] ? new Date(_data["endTime"].toString()) : <any>undefined;
            this.description = _data["description"];
        }
    }

    static fromJS(data: any): AddPauseReq {
        data = typeof data === 'object' ? data : {};
        let result = new AddPauseReq();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["masterId"] = this.masterId;
        data["startTime"] = this.startTime ? this.startTime.toISOString() : <any>undefined;
        data["endTime"] = this.endTime ? this.endTime.toISOString() : <any>undefined;
        data["description"] = this.description;
        return data;
    }
}

export interface IAddPauseReq {
    masterId?: string;
    startTime?: Date;
    endTime?: Date;
    description?: string | undefined;
}

export class CancelPauseReq implements ICancelPauseReq {
    pauseId?: string;

    constructor(data?: ICancelPauseReq) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.pauseId = _data["pauseId"];
        }
    }

    static fromJS(data: any): CancelPauseReq {
        data = typeof data === 'object' ? data : {};
        let result = new CancelPauseReq();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["pauseId"] = this.pauseId;
        return data;
    }
}

export interface ICancelPauseReq {
    pauseId?: string;
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