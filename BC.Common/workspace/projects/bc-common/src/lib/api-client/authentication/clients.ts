/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.10.7.0 (NJsonSchema v10.3.9.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class AuthenticationClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * @param body (optional) 
     * @return AuthenticationResponse
     */
    authenticateByVk(body: AuthenticationCodeRequest | undefined): Observable<AuthenticationResponse> {
        let url_ = this.baseUrl + "/authentication/authenticate-by-vk";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "text/plain"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuthenticateByVk(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuthenticateByVk(<any>response_);
                } catch (e) {
                    return <Observable<AuthenticationResponse>><any>_observableThrow(e);
                }
            } else
                return <Observable<AuthenticationResponse>><any>_observableThrow(response_);
        }));
    }

    protected processAuthenticateByVk(response: HttpResponseBase): Observable<AuthenticationResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = AuthenticationResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = BadAPIResponse.fromJS(resultData400);
            return throwException("BadAPIResponse", status, _responseText, _headers, result400);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<AuthenticationResponse>(<any>null);
    }

    /**
     * @param body (optional) 
     * @return AuthenticationResponse
     */
    authenticateByInstagram(body: AuthenticationCodeRequest | undefined): Observable<AuthenticationResponse> {
        let url_ = this.baseUrl + "/authentication/authenticate-by-instagram";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "text/plain"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuthenticateByInstagram(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuthenticateByInstagram(<any>response_);
                } catch (e) {
                    return <Observable<AuthenticationResponse>><any>_observableThrow(e);
                }
            } else
                return <Observable<AuthenticationResponse>><any>_observableThrow(response_);
        }));
    }

    protected processAuthenticateByInstagram(response: HttpResponseBase): Observable<AuthenticationResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = AuthenticationResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = BadAPIResponse.fromJS(resultData400);
            return throwException("BadAPIResponse", status, _responseText, _headers, result400);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<AuthenticationResponse>(<any>null);
    }

    /**
     * @param body (optional) 
     * @return AuthenticationResponse
     */
    authenticateByGoogle(body: AuthenticationCodeRequest | undefined): Observable<AuthenticationResponse> {
        let url_ = this.baseUrl + "/authentication/authenticate-by-google";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "text/plain"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuthenticateByGoogle(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuthenticateByGoogle(<any>response_);
                } catch (e) {
                    return <Observable<AuthenticationResponse>><any>_observableThrow(e);
                }
            } else
                return <Observable<AuthenticationResponse>><any>_observableThrow(response_);
        }));
    }

    protected processAuthenticateByGoogle(response: HttpResponseBase): Observable<AuthenticationResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = AuthenticationResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = BadAPIResponse.fromJS(resultData400);
            return throwException("BadAPIResponse", status, _responseText, _headers, result400);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<AuthenticationResponse>(<any>null);
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    authenticateByPhoneStep1(body: AuthenticationPhoneRequest | undefined): Observable<void> {
        let url_ = this.baseUrl + "/authentication/authenticate-by-phone-step-1";
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
            return this.processAuthenticateByPhoneStep1(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuthenticateByPhoneStep1(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processAuthenticateByPhoneStep1(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = BadAPIResponse.fromJS(resultData400);
            return throwException("BadAPIResponse", status, _responseText, _headers, result400);
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
     * @return AuthenticationResponse
     */
    authenticateByPhoneStep2(body: AuthenticatebyPhoneStep2Req | undefined): Observable<AuthenticationResponse> {
        let url_ = this.baseUrl + "/authentication/authenticate-by-phone-step-2";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "text/plain"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAuthenticateByPhoneStep2(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAuthenticateByPhoneStep2(<any>response_);
                } catch (e) {
                    return <Observable<AuthenticationResponse>><any>_observableThrow(e);
                }
            } else
                return <Observable<AuthenticationResponse>><any>_observableThrow(response_);
        }));
    }

    protected processAuthenticateByPhoneStep2(response: HttpResponseBase): Observable<AuthenticationResponse> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = AuthenticationResponse.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 400) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = BadAPIResponse.fromJS(resultData400);
            return throwException("BadAPIResponse", status, _responseText, _headers, result400);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<AuthenticationResponse>(<any>null);
    }
}

@Injectable()
export class BalanceClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * @param userId (optional) 
     * @return Success
     */
    increaseBalance(userId: string | undefined): Observable<void> {
        let url_ = this.baseUrl + "/balance/increase-balance";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "UserId": userId !== undefined && userId !== null ? "" + userId : "",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processIncreaseBalance(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processIncreaseBalance(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processIncreaseBalance(response: HttpResponseBase): Observable<void> {
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

export class AuthenticationCodeRequest implements IAuthenticationCodeRequest {
    code?: string | undefined;
    redirectUrl?: string | undefined;
    role?: string | undefined;

    constructor(data?: IAuthenticationCodeRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.code = _data["code"];
            this.redirectUrl = _data["redirectUrl"];
            this.role = _data["role"];
        }
    }

    static fromJS(data: any): AuthenticationCodeRequest {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticationCodeRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        data["redirectUrl"] = this.redirectUrl;
        data["role"] = this.role;
        return data; 
    }
}

export interface IAuthenticationCodeRequest {
    code?: string | undefined;
    redirectUrl?: string | undefined;
    role?: string | undefined;
}

export class AuthenticationResponse implements IAuthenticationResponse {
    /** Access API token */
    token?: string | undefined;
    /** Unique username */
    username?: string | undefined;

    constructor(data?: IAuthenticationResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.token = _data["token"];
            this.username = _data["username"];
        }
    }

    static fromJS(data: any): AuthenticationResponse {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticationResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["token"] = this.token;
        data["username"] = this.username;
        return data; 
    }
}

export interface IAuthenticationResponse {
    /** Access API token */
    token?: string | undefined;
    /** Unique username */
    username?: string | undefined;
}

export class BadAPIResponse implements IBadAPIResponse {
    /** Error code */
    code?: string | undefined;
    /** Error messages */
    messages?: string[] | undefined;

    constructor(data?: IBadAPIResponse) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.code = _data["code"];
            if (Array.isArray(_data["messages"])) {
                this.messages = [] as any;
                for (let item of _data["messages"])
                    this.messages!.push(item);
            }
        }
    }

    static fromJS(data: any): BadAPIResponse {
        data = typeof data === 'object' ? data : {};
        let result = new BadAPIResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["code"] = this.code;
        if (Array.isArray(this.messages)) {
            data["messages"] = [];
            for (let item of this.messages)
                data["messages"].push(item);
        }
        return data; 
    }
}

export interface IBadAPIResponse {
    /** Error code */
    code?: string | undefined;
    /** Error messages */
    messages?: string[] | undefined;
}

export class AuthenticationPhoneRequest implements IAuthenticationPhoneRequest {
    phone?: string | undefined;
    role?: string | undefined;

    constructor(data?: IAuthenticationPhoneRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.phone = _data["phone"];
            this.role = _data["role"];
        }
    }

    static fromJS(data: any): AuthenticationPhoneRequest {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticationPhoneRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["phone"] = this.phone;
        data["role"] = this.role;
        return data; 
    }
}

export interface IAuthenticationPhoneRequest {
    phone?: string | undefined;
    role?: string | undefined;
}

export class AuthenticatebyPhoneStep2Req implements IAuthenticatebyPhoneStep2Req {
    phone?: string | undefined;
    code?: string | undefined;

    constructor(data?: IAuthenticatebyPhoneStep2Req) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.phone = _data["phone"];
            this.code = _data["code"];
        }
    }

    static fromJS(data: any): AuthenticatebyPhoneStep2Req {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticatebyPhoneStep2Req();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["phone"] = this.phone;
        data["code"] = this.code;
        return data; 
    }
}

export interface IAuthenticatebyPhoneStep2Req {
    phone?: string | undefined;
    code?: string | undefined;
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