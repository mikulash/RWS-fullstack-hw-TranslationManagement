/* tslint:disable */
/* eslint-disable */
/**
 * TranslationManagement.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import type { Configuration } from './configuration';
import type { AxiosPromise, AxiosInstance, RawAxiosRequestConfig } from 'axios';
import globalAxios from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import { DUMMY_BASE_URL, assertParamExists, setApiKeyToObject, setBasicAuthToObject, setBearerAuthToObject, setOAuthToObject, setSearchParams, serializeDataIfNeeded, toPathString, createRequestFunction } from './common';
import type { RequestArgs } from './base';
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, BaseAPI, RequiredError, operationServerMap } from './base';

/**
 * 
 * @export
 * @interface CreateTranslationJobDto
 */
export interface CreateTranslationJobDto {
    /**
     * 
     * @type {string}
     * @memberof CreateTranslationJobDto
     */
    'customerName': string;
    /**
     * 
     * @type {string}
     * @memberof CreateTranslationJobDto
     */
    'originalContent': string;
}
/**
 * 
 * @export
 * @interface CreateTranslatorDto
 */
export interface CreateTranslatorDto {
    /**
     * 
     * @type {string}
     * @memberof CreateTranslatorDto
     */
    'name'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof CreateTranslatorDto
     */
    'hourlyRate'?: string | null;
    /**
     * 
     * @type {TranslatorStatus}
     * @memberof CreateTranslatorDto
     */
    'status'?: TranslatorStatus;
    /**
     * 
     * @type {string}
     * @memberof CreateTranslatorDto
     */
    'creditCardNumber'?: string | null;
}


/**
 * 
 * @export
 * @enum {string}
 */

export const JobStatus = {
    New: 'New',
    InProgress: 'InProgress',
    Completed: 'Completed'
} as const;

export type JobStatus = typeof JobStatus[keyof typeof JobStatus];


/**
 * 
 * @export
 * @interface TranslationJobDto
 */
export interface TranslationJobDto {
    /**
     * 
     * @type {number}
     * @memberof TranslationJobDto
     */
    'id'?: number;
    /**
     * 
     * @type {string}
     * @memberof TranslationJobDto
     */
    'customerName'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TranslationJobDto
     */
    'status'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TranslationJobDto
     */
    'originalContent'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TranslationJobDto
     */
    'translatedContent'?: string | null;
    /**
     * 
     * @type {number}
     * @memberof TranslationJobDto
     */
    'price'?: number;
}
/**
 * 
 * @export
 * @interface TranslatorDto
 */
export interface TranslatorDto {
    /**
     * 
     * @type {number}
     * @memberof TranslatorDto
     */
    'id'?: number;
    /**
     * 
     * @type {string}
     * @memberof TranslatorDto
     */
    'name'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof TranslatorDto
     */
    'hourlyRate'?: string | null;
    /**
     * 
     * @type {TranslatorStatus}
     * @memberof TranslatorDto
     */
    'status'?: TranslatorStatus;
    /**
     * 
     * @type {string}
     * @memberof TranslatorDto
     */
    'creditCardNumber'?: string | null;
}


/**
 * 
 * @export
 * @enum {string}
 */

export const TranslatorStatus = {
    Applicant: 'Applicant',
    Certified: 'Certified',
    Deleted: 'Deleted'
} as const;

export type TranslatorStatus = typeof TranslatorStatus[keyof typeof TranslatorStatus];



/**
 * TranslationJobApi - axios parameter creator
 * @export
 */
export const TranslationJobApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {CreateTranslationJobDto} [createTranslationJobDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsCreateJobPost: async (createTranslationJobDto?: CreateTranslationJobDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/jobs/CreateJob`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(createTranslationJobDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string | null} [customer] 
         * @param {File | null} [file] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsCreateJobWithFilePost: async (customer?: string | null, file?: File | null, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/jobs/CreateJobWithFile`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;
            const localVarFormParams = new ((configuration && configuration.formDataCtor) || FormData)();

            if (customer !== undefined) {
                localVarQueryParameter['customer'] = customer;
            }


            if (file !== undefined) { 
                localVarFormParams.append('file', file as any);
            }
    
    
            localVarHeaderParameter['Content-Type'] = 'multipart/form-data';
    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = localVarFormParams;

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsGetJobsGet: async (options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/jobs/GetJobs`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {number} [jobId] 
         * @param {number} [translatorId] 
         * @param {JobStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsUpdateJobStatusPut: async (jobId?: number, translatorId?: number, newStatus?: JobStatus, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/jobs/UpdateJobStatus`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'PUT', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (jobId !== undefined) {
                localVarQueryParameter['jobId'] = jobId;
            }

            if (translatorId !== undefined) {
                localVarQueryParameter['translatorId'] = translatorId;
            }

            if (newStatus !== undefined) {
                localVarQueryParameter['newStatus'] = newStatus;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * TranslationJobApi - functional programming interface
 * @export
 */
export const TranslationJobApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = TranslationJobApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {CreateTranslationJobDto} [createTranslationJobDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiJobsCreateJobPost(createTranslationJobDto?: CreateTranslationJobDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Array<TranslationJobDto>>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiJobsCreateJobPost(createTranslationJobDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslationJobApi.apiJobsCreateJobPost']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {string | null} [customer] 
         * @param {File | null} [file] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiJobsCreateJobWithFilePost(customer?: string | null, file?: File | null, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiJobsCreateJobWithFilePost(customer, file, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslationJobApi.apiJobsCreateJobWithFilePost']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiJobsGetJobsGet(options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Array<TranslationJobDto>>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiJobsGetJobsGet(options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslationJobApi.apiJobsGetJobsGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} [jobId] 
         * @param {number} [translatorId] 
         * @param {JobStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiJobsUpdateJobStatusPut(jobId?: number, translatorId?: number, newStatus?: JobStatus, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiJobsUpdateJobStatusPut(jobId, translatorId, newStatus, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslationJobApi.apiJobsUpdateJobStatusPut']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
    }
};

/**
 * TranslationJobApi - factory interface
 * @export
 */
export const TranslationJobApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = TranslationJobApiFp(configuration)
    return {
        /**
         * 
         * @param {CreateTranslationJobDto} [createTranslationJobDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsCreateJobPost(createTranslationJobDto?: CreateTranslationJobDto, options?: RawAxiosRequestConfig): AxiosPromise<Array<TranslationJobDto>> {
            return localVarFp.apiJobsCreateJobPost(createTranslationJobDto, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string | null} [customer] 
         * @param {File | null} [file] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsCreateJobWithFilePost(customer?: string | null, file?: File | null, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiJobsCreateJobWithFilePost(customer, file, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsGetJobsGet(options?: RawAxiosRequestConfig): AxiosPromise<Array<TranslationJobDto>> {
            return localVarFp.apiJobsGetJobsGet(options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {number} [jobId] 
         * @param {number} [translatorId] 
         * @param {JobStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiJobsUpdateJobStatusPut(jobId?: number, translatorId?: number, newStatus?: JobStatus, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiJobsUpdateJobStatusPut(jobId, translatorId, newStatus, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * TranslationJobApi - object-oriented interface
 * @export
 * @class TranslationJobApi
 * @extends {BaseAPI}
 */
export class TranslationJobApi extends BaseAPI {
    /**
     * 
     * @param {CreateTranslationJobDto} [createTranslationJobDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslationJobApi
     */
    public apiJobsCreateJobPost(createTranslationJobDto?: CreateTranslationJobDto, options?: RawAxiosRequestConfig) {
        return TranslationJobApiFp(this.configuration).apiJobsCreateJobPost(createTranslationJobDto, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {string | null} [customer] 
     * @param {File | null} [file] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslationJobApi
     */
    public apiJobsCreateJobWithFilePost(customer?: string | null, file?: File | null, options?: RawAxiosRequestConfig) {
        return TranslationJobApiFp(this.configuration).apiJobsCreateJobWithFilePost(customer, file, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslationJobApi
     */
    public apiJobsGetJobsGet(options?: RawAxiosRequestConfig) {
        return TranslationJobApiFp(this.configuration).apiJobsGetJobsGet(options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {number} [jobId] 
     * @param {number} [translatorId] 
     * @param {JobStatus} [newStatus] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslationJobApi
     */
    public apiJobsUpdateJobStatusPut(jobId?: number, translatorId?: number, newStatus?: JobStatus, options?: RawAxiosRequestConfig) {
        return TranslationJobApiFp(this.configuration).apiJobsUpdateJobStatusPut(jobId, translatorId, newStatus, options).then((request) => request(this.axios, this.basePath));
    }
}



/**
 * TranslatorManagementApi - axios parameter creator
 * @export
 */
export const TranslatorManagementApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {CreateTranslatorDto} [createTranslatorDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementAddTranslatorPost: async (createTranslatorDto?: CreateTranslatorDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/TranslatorsManagement/AddTranslator`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(createTranslatorDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string | null} [name] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementGetTranslatorsByNameGet: async (name?: string | null, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/TranslatorsManagement/GetTranslatorsByName`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (name !== undefined) {
                localVarQueryParameter['name'] = name;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementGetTranslatorsGet: async (options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/TranslatorsManagement/GetTranslators`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {number} [translatorId] 
         * @param {TranslatorStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementUpdateTranslatorStatusPut: async (translatorId?: number, newStatus?: TranslatorStatus, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/TranslatorsManagement/UpdateTranslatorStatus`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'PUT', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (translatorId !== undefined) {
                localVarQueryParameter['translatorId'] = translatorId;
            }

            if (newStatus !== undefined) {
                localVarQueryParameter['newStatus'] = newStatus;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * TranslatorManagementApi - functional programming interface
 * @export
 */
export const TranslatorManagementApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = TranslatorManagementApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {CreateTranslatorDto} [createTranslatorDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiTranslatorsManagementAddTranslatorPost(createTranslatorDto?: CreateTranslatorDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiTranslatorsManagementAddTranslatorPost(createTranslatorDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslatorManagementApi.apiTranslatorsManagementAddTranslatorPost']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {string | null} [name] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiTranslatorsManagementGetTranslatorsByNameGet(name?: string | null, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Array<TranslatorDto>>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiTranslatorsManagementGetTranslatorsByNameGet(name, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslatorManagementApi.apiTranslatorsManagementGetTranslatorsByNameGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiTranslatorsManagementGetTranslatorsGet(options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Array<TranslatorDto>>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiTranslatorsManagementGetTranslatorsGet(options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslatorManagementApi.apiTranslatorsManagementGetTranslatorsGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} [translatorId] 
         * @param {TranslatorStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId?: number, newStatus?: TranslatorStatus, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId, newStatus, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['TranslatorManagementApi.apiTranslatorsManagementUpdateTranslatorStatusPut']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
    }
};

/**
 * TranslatorManagementApi - factory interface
 * @export
 */
export const TranslatorManagementApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = TranslatorManagementApiFp(configuration)
    return {
        /**
         * 
         * @param {CreateTranslatorDto} [createTranslatorDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementAddTranslatorPost(createTranslatorDto?: CreateTranslatorDto, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiTranslatorsManagementAddTranslatorPost(createTranslatorDto, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string | null} [name] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementGetTranslatorsByNameGet(name?: string | null, options?: RawAxiosRequestConfig): AxiosPromise<Array<TranslatorDto>> {
            return localVarFp.apiTranslatorsManagementGetTranslatorsByNameGet(name, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementGetTranslatorsGet(options?: RawAxiosRequestConfig): AxiosPromise<Array<TranslatorDto>> {
            return localVarFp.apiTranslatorsManagementGetTranslatorsGet(options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {number} [translatorId] 
         * @param {TranslatorStatus} [newStatus] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId?: number, newStatus?: TranslatorStatus, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId, newStatus, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * TranslatorManagementApi - object-oriented interface
 * @export
 * @class TranslatorManagementApi
 * @extends {BaseAPI}
 */
export class TranslatorManagementApi extends BaseAPI {
    /**
     * 
     * @param {CreateTranslatorDto} [createTranslatorDto] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslatorManagementApi
     */
    public apiTranslatorsManagementAddTranslatorPost(createTranslatorDto?: CreateTranslatorDto, options?: RawAxiosRequestConfig) {
        return TranslatorManagementApiFp(this.configuration).apiTranslatorsManagementAddTranslatorPost(createTranslatorDto, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {string | null} [name] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslatorManagementApi
     */
    public apiTranslatorsManagementGetTranslatorsByNameGet(name?: string | null, options?: RawAxiosRequestConfig) {
        return TranslatorManagementApiFp(this.configuration).apiTranslatorsManagementGetTranslatorsByNameGet(name, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslatorManagementApi
     */
    public apiTranslatorsManagementGetTranslatorsGet(options?: RawAxiosRequestConfig) {
        return TranslatorManagementApiFp(this.configuration).apiTranslatorsManagementGetTranslatorsGet(options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {number} [translatorId] 
     * @param {TranslatorStatus} [newStatus] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TranslatorManagementApi
     */
    public apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId?: number, newStatus?: TranslatorStatus, options?: RawAxiosRequestConfig) {
        return TranslatorManagementApiFp(this.configuration).apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId, newStatus, options).then((request) => request(this.axios, this.basePath));
    }
}



