import { Entity } from "./base_entity/Entity";
import { Membership_Permission } from "./membership_permission.model";

export class Membership_Role extends Entity {
  public Permissions: Membership_Permission[];
  public RoleName: string;
  public IsAdmin: boolean;
}
