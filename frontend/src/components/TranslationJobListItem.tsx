import {JobStatus, TranslationJobDto} from "../../generated-api";
import {translationJobApi} from "../ApiClientConfig.ts";
import {useMutation, useQueryClient} from "@tanstack/react-query";

export const TranslationJobListItem = ({job}: {
    job: TranslationJobDto,
}) => {
    const queryClient = useQueryClient();
    const statusMutation = useMutation({
            mutationFn: async (newStatus: JobStatus) => {
                await translationJobApi.apiJobsUpdateJobStatusPut(
                    job.id!, newStatus
                );
            },
            onSuccess: () => {
                void queryClient.invalidateQueries({
                    queryKey: ['translation-jobs'],
                });
            },
            onError: (error: Error) => {
                console.error('Failed to update job status', error);
            }
        }
    );

    return (
        <li key={job.id}>
            <p>Customer Name: {job.customerName}</p>
            <p>Status: {job.status}</p>
            <p>Original Content: {job.originalContent}</p>
            <p>Translated Content: {job.translatedContent}</p>
            <p>Price: {job.price}</p>
            <div>
                <label>Update Status:</label>
                <select
                    value={job.status as JobStatus}
                    onChange={(e) =>
                        statusMutation.mutate(e.target.value as JobStatus)
                    }
                >
                    <option value={JobStatus.New}>New</option>
                    <option value={JobStatus.InProgress}>InProgress</option>
                    <option value={JobStatus.Completed}>Completed</option>
                </select>
            </div>
        </li>);
}