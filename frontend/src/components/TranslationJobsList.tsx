import React from 'react';
import {translationJobApi} from "../ApiClientConfig.ts";
import {TranslationJobListItem} from "./TranslationJobListItem.tsx";
import {useQuery} from "@tanstack/react-query";

const TranslationJobsList: React.FC = () => {

    const {isPending, error, data} = useQuery({
        queryKey: ['translation-jobs'],
        queryFn: async () => {
            return await translationJobApi.apiJobsGetJobsGet();
        }
    })


    if (isPending) {
        return <div>Loading translation jobs...</div>;
    }

    if (error) {
        return <div>{error.message}</div>;
    }

    return (
        <div>
            <h1>Translation Jobs List</h1>
            <ul>
                {data.data.map((job) => (
                    <TranslationJobListItem job={job}/>
                ))}
            </ul>
        </div>
    );
};

export default TranslationJobsList;
