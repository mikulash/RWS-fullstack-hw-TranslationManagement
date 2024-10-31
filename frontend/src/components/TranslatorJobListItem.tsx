import {JobStatus, TranslationJobDto} from "../../generated-api";

export const TranslatorJobListItem = ({job, handleStatusChange}: {
    job: TranslationJobDto,
    handleStatusChange: (id: number, status: JobStatus, translatorId?: number) => void
}) => {
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
                        handleStatusChange(job.id!, e.target.value as JobStatus)
                    }
                >
                    <option value={JobStatus.New}>New</option>
                    <option value={JobStatus.InProgress}>InProgress</option>
                    <option value={JobStatus.Completed}>Completed</option>
                </select>
            </div>
        </li>);
}