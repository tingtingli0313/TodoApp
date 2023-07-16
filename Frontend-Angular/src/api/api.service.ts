import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { endpoints } from './api-endpoints-map';
import { ApiEndpointKey, ApiEndpoints, QueryParams } from './api.model';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly endpoints: ApiEndpoints;
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
    this.endpoints = endpoints;
  }

  /**
   * Send GET request
   * @param endpoint
   * @param params
   * @returns Observable<R>
   */
  get<R>(
    endpoint: ApiEndpointKey,
    params?: QueryParams,
    options?: { [key: string]: any }
  ): Observable<R> {
    let url = `${this.baseUrl}${this.endpoints[ApiEndpointKey.TODOITEMS].path}`;
   
    return this.http.get<R>(url, options);
  }


  /**
   * Send PUT request
   * @param endpoint
   * @param payload
   * @param params
   * @returns Observable<R>
   */
  put<P, R>(
    endpoint: ApiEndpointKey,
    payload?: any,
    params?: QueryParams
  ): Observable<R> {
    let url = `${this.baseUrl}${this.endpoints[ApiEndpointKey.TODOITEMS].path}/${payload?.id}`
    return this.http.put<R>(url, payload, {
      context: null
    });
  }

  /**
   * Send PATCH request
   * @param endpoint
   * @param payload
   * @param params
   * @returns Observable<R>
   */
  patch<P, R>(
    endpoint: ApiEndpointKey,
    payload?: any,
    params?: QueryParams
  ): Observable<R> {
    let url = `${this.baseUrl}${this.endpoints[ApiEndpointKey.TODOITEMS].path}/${payload?.id}`
    return this.http.patch<R>((url), payload, {
      context: null
    });
  }

  /**
   * Send POST request
   * @param endpoint
   * @param payload
   * @param params
   * @returns Observable<R>
   */
  post<P, R>(
    endpoint: ApiEndpointKey,
    payload?: P,
    params?: QueryParams
  ): Observable<R> {
    let url = `${this.baseUrl}${this.endpoints[ApiEndpointKey.TODOITEMS].path}`;

    return this.http.post<R>(url, payload, {
      context: null
    });
  }

  /**
   * Send DELETE request
   * @param endpoint
   * @param params
   * @returns Observable<R>
   */
  delete<R>(endpoint: ApiEndpointKey, params?: QueryParams): Observable<R> {
    return this.http.delete<R>(this.getApiUrl(endpoint, params), {
      context: null
    });
  }

  /**
   * Get path for a specific API endpoint, including query params
   * @param name
   * @param params
   * @returns API Path
   */
  getApiUrl(name: ApiEndpointKey, params?: QueryParams): string {
    let { path } = this.endpoints[name];

    if (params) {
      Object.keys(params).forEach((key) => {
        path = path.replace(`{${key}}`, encodeURIComponent(params[key]));
      });
    }

    // Check if the param values replaced correctly, null if correct
    const invalid = /\{\w+\}/.exec(path);

    if (invalid) {
      // array means regex above is still found in path, throw error
      throw new Error(`Params unmatched in this path: ${path}`);
    }

    return path;
  }
}
