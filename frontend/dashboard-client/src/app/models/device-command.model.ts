export interface DeviceCommand {
  entityId: string;
  action: string;
  data?: Record<string, any>;
}
