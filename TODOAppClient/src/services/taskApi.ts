const _url = "https://localhost:7121/api/Task/"

export const GetAllTasks = () => {
    fetch(_url + "GetAll")
        .then(res => {
            return res.json()
        })
        .then(data => console.log(data))
}