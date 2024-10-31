import {TranslatorDto, TranslatorStatus} from "../../generated-api";

export const TranslatorListItem = ({translator, handleStatusChange}: {
    translator: TranslatorDto,
    handleStatusChange: (id: number, status: TranslatorStatus) => void
}) => {
    return (
        <li key={translator.id}>
            <p>Name: {translator.name}</p>
            <p>Hourly Rate: {translator.hourlyRate}</p>
            <p>Status: {translator.status}</p>
            <div>
                <label>Update Status:</label>
                <select
                    value={translator.status}
                    onChange={(e) =>
                        handleStatusChange(translator.id!, e.target.value as TranslatorStatus)
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
