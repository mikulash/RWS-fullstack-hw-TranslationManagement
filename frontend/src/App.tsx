import React from 'react';
import TranslatorsList from "./components/TranslatorList.tsx";
import TranslationJobsList from "./components/TranslationJobsList.tsx";
import NewTranslatorForm from "./components/NewTranslatorForm.tsx";
import AddTranslationJobForm from "./components/AddTranslationJobForm.tsx";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";


const queryClient = new QueryClient()


const App: React.FC = () => {
    return (
        <QueryClientProvider client={queryClient}>

            <div className="App" style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                <h1>Translation Management System</h1>
                <TranslatorsList/>
                <NewTranslatorForm/>
                <Divider/>
                <TranslationJobsList/>
                <AddTranslationJobForm/>

            </div>
        </QueryClientProvider>
    );
};


// create component whole width of page divider
const Divider: React.FC = () => {
    return <hr style={{width: '100%'}}/>;
}

export default App;
