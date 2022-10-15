export class QueryResponse<T> {
  public totalCount: number;
  public result: T;
  public message: string;
  public isSuccess: boolean;
}
