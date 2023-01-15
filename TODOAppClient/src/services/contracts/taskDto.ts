import { Status } from "./enums";

export default interface TaskDto {
    id: string;
    name: string;
    priority: number;
    status: Status;
}