import {Configuration, TranslatorManagementApi} from "../generated-api";


const apiConfig = new Configuration({
    basePath: 'http://localhost:5000', // Set base URL to your backend
});

const translatorApi = new TranslatorManagementApi(apiConfig);

export default translatorApi;