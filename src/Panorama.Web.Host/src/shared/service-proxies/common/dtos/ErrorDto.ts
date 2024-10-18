export interface IErrorDto {
    errorMessage: string;
}

export class ErrorDto implements IErrorDto {
    errorMessage: string = undefined;
}