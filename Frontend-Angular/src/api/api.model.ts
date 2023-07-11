// TODO: This currently uses the numeric value which is difficult to understand.
// Ideally this would reference the path directly
/*
 * Add new endpoint key here
 */
export enum ApiEndpointKey {
    /**
     * Customer details endpoint
     */
    TODOITEMS,
    POSTITEM,
  }
  
  export interface ApiEndpoint {
    /**
     * Query params and path to the resources
     * eg. /v1/accounts/{accountId}
     */
    path: string;
  
    /**
     * https://angular.io/api/common/http/HttpContext
     */
    context?: any;
  }
  
  export type ApiEndpoints = {
    [key in ApiEndpointKey]: ApiEndpoint;
  };
  
  export interface QueryParams {
    [key: string]: string | number;
  }
  