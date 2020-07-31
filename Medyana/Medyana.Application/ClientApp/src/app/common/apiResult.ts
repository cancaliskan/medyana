export class ApiResult<TResult> {
  result: TResult;
  isSucceed: boolean;
  errorMessage: string;
  successMessage: string;
}
