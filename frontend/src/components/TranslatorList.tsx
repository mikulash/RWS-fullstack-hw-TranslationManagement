import React, {useEffect, useState} from 'react';
import {TranslatorDto, TranslatorStatus} from "../../generated-api";
import {translatorApi} from "../ApiClientConfig.ts";
import {TranslatorListItem} from "./TranslatorListItem.tsx";


const TranslatorsList: React.FC = () => {
    const [translators, setTranslators] = useState<TranslatorDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchTranslators = async () => {
            try {
                const response = await translatorApi.apiTranslatorsManagementGetTranslatorsGet();
                setTranslators(response.data);
            } catch (err) {
                setError('Failed to fetch translators');
                console.error(err);
            } finally {
                setLoading(false);
            }
        };

        fetchTranslators();
    }, []);

    const handleStatusChange = async (translatorId: number, newStatus: TranslatorStatus) => {
        try {
            await translatorApi.apiTranslatorsManagementUpdateTranslatorStatusPut(translatorId, newStatus);
            setTranslators((prevTranslators) =>
                prevTranslators.map((translator) =>
                    translator.id === translatorId ? {...translator, status: newStatus} : translator
                )
            );
        } catch (err) {
            console.error('Failed to update translator status', err);
        }
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Translators List</h1>
            <ul>
                {translators.map((translator) => (
                    <TranslatorListItem
                        key={translator.id}
                        translator={translator}
                        handleStatusChange={handleStatusChange}
                    />
                ))}
            </ul>
        </div>
    );

};

export default TranslatorsList;
