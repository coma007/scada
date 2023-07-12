
export const formatDate = (date: Date) => {
    const formattedDate = new Date(date).toLocaleString("sr-RS", {
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    });
    return formattedDate;
}


export const formatTime = (date: Date) => {
    const formattedDate = (new Date(date)).toLocaleString("sr-RS", {
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
    });
    return formattedDate;
}
