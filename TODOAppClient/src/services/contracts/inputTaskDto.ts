import { Status } from "./enums";

export default interface InputTaskDto {
    name: string;
    priority: number;
    status: Status;
}