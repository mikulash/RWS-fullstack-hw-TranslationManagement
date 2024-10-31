import React, {useState} from 'react';
import {CreateTranslationJobDto} from "../../generated-api";
import {translationJobApi} from "../ApiClientConfig.ts";
import {useMutation, useQueryClient} from "@tanstack/react-query";

const NewTranslationJobForm: React.FC = () => {
    const [customerName, setCustomerName] = useState<string>('');
    const [originalContent, setOriginalContent] = useState<string>('');
    const [message, setMessage] = useState<string>('');

    const queryClient = useQueryClient();
    const newTranslationJobMutation = useMutation({
        mutationFn: async (newJob: CreateTranslationJobDto) => {
            await translationJobApi.apiJobsCreateJobPost(newJob);
        },
        onSuccess: () => {
            void queryClient.invalidateQueries({
                queryKey: ['translation-jobs'],
            });
            setMessage('Translation job created successfully');
        },
        onError: (error: Error) => {
            console.error('Failed to create translation job', error);
            setMessage('Failed to create translation job');
        }
    });


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const newJob: CreateTranslationJobDto = {
            customerName,
            originalContent,
        };

        newTranslationJobMutation.mutate(newJob);
    };

    return (
        <div>
            <h2>Add New Translation Job</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Customer Name:</label><br/>
                    <input
                        type="text"
                        value={customerName}
                        onChange={(e) => setCustomerName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Original Content:</label><br/>
                    <textarea
                        value={originalContent}
                        onChange={(e) => setOriginalContent(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Add Translation Job</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default NewTranslationJobForm;
