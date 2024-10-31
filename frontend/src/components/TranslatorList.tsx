import {useEffect, useState} from "react";
import {TranslatorDto} from "../../generated-api";
import translatorApi from "../ApiClientConfig.ts";

export const TranslatorList: React.FC = () => {
    const [translators, setTranslators] = useState<TranslatorDto[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchTranslators = async () => {
            try {
                const response = await translatorApi.apiTranslatorsManagementGetTranslatorsGet();
                setTranslators(response.data);
            } catch (error) {
                console.error('Failed to fetch translators:', error);
                setError('Could not retrieve translators from backend');
            }
        };

        fetchTranslators();
    }, []);

    return (
        <div>
            <h2>Translator List</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <ul>
                {translators.map((translator) => (
                    <li key={translator.id}>
                        {translator.name} - {translator.status}
                    </li>
                ))}
            </ul>
        </div>
    );
};

