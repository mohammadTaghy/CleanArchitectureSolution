import { Permission } from "./Permission.model";

export class Role {
  public Permissions: Permission[];
  constructor(public RoleName: string) { }
}
