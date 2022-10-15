import { HierarchyEntity } from "./base_entity/HierarchyEntity";
//import { ITreeNode } from "../commonComponent/componnent/material_flatTree"

export class Membership_Permission extends HierarchyEntity<Membership_Permission> {
  
  public name: string;
  public title: string;
  public commandName: string;
  public featureType: number;
  public isActive: string;
  public hasPermission: boolean;
}
