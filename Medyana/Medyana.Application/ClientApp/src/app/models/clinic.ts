  export interface Result {
      id: number;
      name: string;
      equipments: any[];
      createdDate?: Date;
      updateDate?: Date;
      deletedDate?: any;
      isActive: boolean;
  }

  export interface Clinic {
      result: Result[];
      isSucceed: boolean;
      errorMessage?: any;
  }
