import { Entity } from "./Entity";

export class HierarchyEntity<T> extends Entity {
  public parentId: number;
  public hasChild: boolean;
  public childList: T[];
}
