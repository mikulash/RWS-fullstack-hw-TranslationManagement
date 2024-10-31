import React from 'react';
import TranslatorsList from "./components/TranslatorList.tsx";
import TranslationJobsList from "./components/TranslationJobsList.tsx";
import AddTranslatorForm from "./components/AddTranslatorForm.tsx";
import AddTranslationJobForm from "./components/AddTranslationJobForm.tsx";


const App: React.FC = () => {
    return (
        <div className="App" style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
            <h1>Translation Management System</h1>
            <AddTranslatorForm/>
            <Divider/>
            <AddTranslationJobForm/>
            <Divider/>
            <TranslatorsList/>
            <Divider/>
            <TranslationJobsList/>
        </div>
    );
};


// create component whole width of page divider
const Divider: React.FC = () => {
    return <hr style={{width: '100%'}}/>;
}

export default App;
