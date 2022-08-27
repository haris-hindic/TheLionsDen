import 'package:json_annotation/json_annotation.dart';
import 'package:the_lions_den_mobile/model/reservation/payment_details_insert_request.dart';
part 'reservation_insert_request.g.dart';

@JsonSerializable()
class ReservationInsertRequest {
  DateTime? arrival;
  DateTime? departure;
  double? totalPrice;
  String? estimatedArrivalTime;
  int? userId;
  int? roomId;
  PaymetDetailsInsertRequest? paymentDetails;
  List<int>? facilityIds;

  ReservationInsertRequest();

  factory ReservationInsertRequest.fromJson(Map<String, dynamic> json) =>
      _$ReservationInsertRequestFromJson(json);

  /// Connect the generated [_$ReservationInsertRequestToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$ReservationInsertRequestToJson(this);
}
