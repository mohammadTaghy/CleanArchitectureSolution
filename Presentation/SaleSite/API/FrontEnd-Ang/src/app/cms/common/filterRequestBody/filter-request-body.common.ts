export class FilterRequestBody {
  constructor(public ColumnName: string, public Comparison: string, public Value: string) { }
}
export class QueryRequestBody {
  constructor(public Index: number, public PageSize: number, public FilterData: FilterRequestBody[] = null) { }
}
