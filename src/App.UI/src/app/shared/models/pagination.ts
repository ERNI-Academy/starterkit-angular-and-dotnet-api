export interface Pagination<T = any> {
  page: number;
  pageSize: number;
  /** Field to order by Ascendant */
  orderBy?: string;
  /** Field to order by Descendant */
  orderByDescendant?: keyof T;
}

export interface PaginationResult<T> {
  elements: T[];
  totalElements: number;
}
