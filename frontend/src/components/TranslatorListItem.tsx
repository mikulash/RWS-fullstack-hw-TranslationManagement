import {TranslatorDto, TranslatorStatus} from "../../generated-api";
import {translatorApi} from "../ApiClientConfig.ts";
import {useMutation, useQueryClient} from "@tanstack/react-query";

export const TranslatorListItem = ({translator}: {
    translator: TranslatorDto,
}) => {

    const queryClient = useQueryClient();
    const statusMutation = useMutation({
        mutationFn: async (newStatus: TranslatorStatus) => {
            await translatorApi.apiTranslatorsManagementUpdateTranslatorStatusPut(
                translator.id, newStatus
            );
        },
        onSuccess: () => {
            void queryClient.invalidateQueries({
                queryKey: ['translators'],
            });
        },
        onError: (error: Error) => {
            console.error('Failed to update translator status', error);
        }
    });
    return (
        <li key={translator.id}>
            <p>Name: {translator.name}</p>
            <p>Hourly Rate: {translator.hourlyRate}</p>
            <p>Status: {translator.status}</p>
            <div>
                <label>Update Status:</label>
                <select
                    value={translator.status}
                    onChange={(e) => {
                        statusMutation.mutate(e.target.value as TranslatorStatus);
                    }
                    }
                >
                    <option value={TranslatorStatus.Applicant}>Applicant</option>
                    <option value={TranslatorStatus.Certified}>Certified</option>
                    <option value={TranslatorStatus.Deleted}>Deleted</option>
                </select>
            </div>
        </li>
    );
};
