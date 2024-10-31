import React, {useEffect, useState} from 'react';
import {JobStatus, TranslationJobDto} from "../../generated-api";
import {translationJobApi} from "../ApiClientConfig.ts";


const TranslationJobsList: React.FC = () => {
    const [jobs, setJobs] = useState<TranslationJobDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchJobs = async () => {
            try {
                const response = await translationJobApi.apiJobsGetJobsGet();
                setJobs(response.data);
            } catch (err) {
                setError('Failed to fetch translation jobs');
                console.error(err);
            } finally {
                setLoading(false);
            }
        };

        fetchJobs();
    }, []);

    const handleStatusChange = async (
        jobId: number,
        newStatus: JobStatus,
        translatorId?: number
    ) => {
        try {
            await translationJobApi.apiJobsUpdateJobStatusPut(jobId, translatorId, newStatus);
            // Update local state
            setJobs((prevJobs) =>
                prevJobs.map((job) =>
                    job.id === jobId ? {...job, status: newStatus} : job
                )
            );
        } catch (err) {
            console.error('Failed to update job status', err);
        }
    };

    if (loading) {
        return <div>Loading translation jobs...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Translation Jobs List</h1>
            <ul>
                {jobs.map((job) => (
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
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default TranslationJobsList;
