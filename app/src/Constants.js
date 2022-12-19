export const baseUrl = 'https://localhost:7124';

//ref: https://stackoverflow.com/a/55556258/725957
export const currencyFormat = (num) => {
    return '$' + num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
};

export const dateFormat = (dob) => {
    let parsedDate = new Date(Date.parse(dob));
    return (parsedDate.getMonth() + 1) + '/' + parsedDate.getDate() + '/' + parsedDate.getFullYear();
}