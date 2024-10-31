import React from 'react';
import {translatorApi} from "../ApiClientConfig.ts";
import {TranslatorListItem} from "./TranslatorListItem.tsx";
import {useQuery} from "@tanstack/react-query";


const TranslatorsList: React.FC = () => {

    const {isPending, error, data} = useQuery({
        queryKey: ['translators'],
        queryFn: async () => {
            return await translatorApi.apiTranslatorsManagementGetTranslatorsGet();
        }
    });

    if (isPending) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error.message}</div>;
    }

    return (
        <div>
            <h1>Translators List</h1>
            <ul>
                {data.data.map((translator) => (
                    <TranslatorListItem
                        key={translator.id}
                        translator={translator}
                    />
                ))}
            </ul>
        </div>
    );

};

export default TranslatorsList;
