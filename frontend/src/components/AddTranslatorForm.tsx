import React, {useState} from 'react';
import {CreateTranslatorDto, TranslatorStatus} from "../../generated-api";
import {translatorApi} from "../ApiClientConfig.ts";


const AddTranslatorForm: React.FC = () => {
    const [name, setName] = useState<string>('');
    const [hourlyRate, setHourlyRate] = useState<string>('');
    const [status, setStatus] = useState<TranslatorStatus>(TranslatorStatus.Applicant);
    const [creditCardNumber, setCreditCardNumber] = useState<string>('');
    const [isUpdateSuccessMsg, setIsUpdateSuccessMsg] = useState<string>('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const newTranslator: CreateTranslatorDto = {
            name,
            hourlyRate,
            status,
            creditCardNumber,
        };

        try {
            await translatorApi.apiTranslatorsManagementAddTranslatorPost(newTranslator);
            setIsUpdateSuccessMsg('Translator added successfully');
        } catch (err) {
            setIsUpdateSuccessMsg('Failed to add translator');
            console.error(err);
        }
    };

    return (
        <div>
            <h2>Add New Translator</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name:</label><br/>
                    <input type="text" value={name} onChange={(e) => setName(e.target.value)} required/>
                </div>
                <div>
                    <label>Hourly Rate:</label><br/>
                    <input type="text" value={hourlyRate} onChange={(e) => setHourlyRate(e.target.value)}/>
                </div>
                <div>
                    <label>Status:</label><br/>
                    <select value={status} onChange={(e) => setStatus(e.target.value as TranslatorStatus)}>
                        <option value={TranslatorStatus.Applicant}>Applicant</option>
                        <option value={TranslatorStatus.Certified}>Certified</option>
                        <option value={TranslatorStatus.Deleted}>Deleted</option>
                    </select>
                </div>
                <div>
                    <label>Credit Card Number:</label><br/>
                    <input
                        type="text"
                        value={creditCardNumber}
                        onChange={(e) => setCreditCardNumber(e.target.value)}
                    />
                </div>
                <button type="submit">Add Translator</button>
            </form>
            {isUpdateSuccessMsg && <p>{isUpdateSuccessMsg}</p>}
        </div>
    );
};

export default AddTranslatorForm;
