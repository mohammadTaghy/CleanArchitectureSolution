import { Entity } from "./Entity";

export class HierarchyEntity<T> extends Entity {
  public parentId: number = null;
  public hasChild: boolean = false;
  public childList: T[];
  //public setChildList(childs: T[]) {
  //  this.childList = childs;
  //}
}
