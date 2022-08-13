import { UserProfile } from "./userProfile.model";

export class User {
  
  public UserProfile: UserProfile;
  constructor(public UserName: string, public UserCode: string, public MobileNumber: string, public Email: string,
    public IsMobileNumberConfirmed: boolean, public IsEmailConfirmed: boolean, public IsUserConfirm: boolean,
    public ManagerConfirm: number, public DeviceId: string, public Token: string) { }
}
