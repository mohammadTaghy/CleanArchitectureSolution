import { Entity } from "./base_entity/Entity";
import { Membership_Role } from "./membership_roles.model";
import { Membership_UserProfile } from "./membership_userProfile.model";

export class Membership_User extends Entity {

  public UserRoles: Membership_Role[];
  public UserProfile: Membership_UserProfile;
  public UserName: string;
  public UserCode: string;
  public MobileNumber: string;
  public Email: string;
  public IsMobileNumberConfirmed: boolean;
  public IsEmailConfirmed: boolean;
  public IsUserConfirm: boolean;
  public ManagerConfirm: number;
  public DeviceId: string;
}
