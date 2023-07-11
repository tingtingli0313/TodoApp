import { ApiEndpointKey, ApiEndpoints } from './api.model';

export const endpoints: ApiEndpoints = {
    [ApiEndpointKey.TODOITEMS]: {
      path: 'https://localhost:44397/api/todoitems',
    },
    [ApiEndpointKey.POSTITEM]: {
        path: 'https://localhost:44397/api/todoitems',
    },
}